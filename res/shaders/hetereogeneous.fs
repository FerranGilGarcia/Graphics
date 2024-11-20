#version 450 core

uniform vec4 u_backgroundColor;          // Color de fondo (application)
uniform vec3 u_boxMin;                   // Coordenadas mínimas de la AABB 
uniform vec3 u_boxMax;                   // Coordenadas máximas de la AABB

uniform float u_maxDistance;             // Distancia máxima para el ray marching
uniform float u_numSteps;                  // Número de pasos de ray marching
uniform float u_absorptionCoefficient;   // Coeficiente de absorción base (escala para la densidad)
//uniform float time;                    //Pusimos esta variable cuando pusimos otro ruido para que se moviera con el tiempo 
uniform float u_noise_scale;
uniform float u_noise_detail; 

uniform vec3 u_cameraPosition;           // Posición de la cámara 

in vec3 v_position;
out vec4 FragColor;

// Función para calcular la intersección con un AABB (la misma que en volumen)
vec2 intersectAABB(vec3 rayOrigin, vec3 rayDir, vec3 boxMin, vec3 boxMax) {
    vec3 tMin = (boxMin - rayOrigin) / rayDir;
    vec3 tMax = (boxMax - rayOrigin) / rayDir;
    vec3 t1 = min(tMin, tMax);
    vec3 t2 = max(tMin, tMax);
    float tNear = max(max(t1.x, t1.y), t1.z);
    float tFar = min(min(t2.x, t2.y), t2.z);
    return vec2(tNear, tFar);
}

// Noise functions
float hash1( float n )
{
    return fract( n*17.0*fract( n*0.3183099 ) );
}

float noise( vec3 x )
{
    vec3 p = floor(x);
    vec3 w = fract(x);
    
    vec3 u = w*w*w*(w*(w*6.0-15.0)+10.0);
    
    float n = p.x + 317.0*p.y + 157.0*p.z;
    
    float a = hash1(n+0.0);
    float b = hash1(n+1.0);
    float c = hash1(n+317.0);
    float d = hash1(n+318.0);
    float e = hash1(n+157.0);
    float f = hash1(n+158.0);
    float g = hash1(n+474.0);
    float h = hash1(n+475.0);

    float k0 =   a;
    float k1 =   b - a;
    float k2 =   c - a;
    float k3 =   e - a;
    float k4 =   a - b - c + d;
    float k5 =   a - c - e + g;
    float k6 =   a - b - e + f;
    float k7 = - a + b + c - d + e - f - g + h;

    return -1.0+2.0*(k0 + k1*u.x + k2*u.y + k3*u.z + k4*u.x*u.y + k5*u.y*u.z + k6*u.z*u.x + k7*u.x*u.y*u.z);
}

#define MAX_OCTAVES 16

float fractal_noise( vec3 P, float detail )
{
    float fscale = 1.0;
    float amp = 1.0;
    float sum = 0.0;
    float octaves = clamp(detail, 0.0, 16.0);
    int n = int(octaves);

    for (int i = 0; i <= MAX_OCTAVES; i++) {
        if (i > n) continue;
        float t = noise(fscale * P);
        sum += t * amp;
        amp *= 0.5;
        fscale *= 2.0;
    }

    return sum;
}

float cnoise( vec3 P, float scale, float detail )
{
    P *= scale;
    return clamp(fractal_noise(P, detail), 0.0, 1.0);
} 

void main() {
    // Inicializar el rayo
    vec3 rayOrigin = u_cameraPosition;
    vec3 rayDirection = normalize(v_position - rayOrigin);

    // Calcular la intersección inicial con el volumen
    vec2 intersection = intersectAABB(rayOrigin, rayDirection, u_boxMin, u_boxMax);
    
    // Variables para ray marching
    float stepSize = (intersection.y - intersection.x) / float(u_numSteps);
    float opticalThickness = 0.0;

    // Posición inicial del rayo dentro del volumen
    vec3 samplePosition = rayOrigin + intersection.x * rayDirection;

    // Ray marching loop
    for (int i = 0; i < u_numSteps; i++) {
    
        // Obtener densidad usando la función de ruido
        float density = cnoise(samplePosition,u_noise_scale,u_noise_detail); 
        // Calcular el coeficiente de absorción en función de la densidad
        float localAbsorption = density * u_absorptionCoefficient;

        // Acumular el espesor óptico
        opticalThickness += localAbsorption * stepSize;

        // Avanzar a la siguiente posición de la muestra
        samplePosition += rayDirection * stepSize;
    }
    // Calcular la transmitancia usando la ley de Beer-Lambert
    float transmittance = exp(-opticalThickness);

    // Calcular el color final aplicando la transmitancia al color de fondo
    FragColor = u_backgroundColor * transmittance;
}

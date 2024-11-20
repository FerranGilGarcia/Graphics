#version 450 core

// Uniformes de entrada
uniform vec4 u_color;
uniform vec4 u_backgroundColor;         // Color de fondo (application)
uniform float u_absorptionCoefficient;  // Coeficiente de absorción (material)
uniform vec3 u_boxMin;                  // Coordenadas mínimas de la AABB
uniform vec3 u_boxMax;                  // Coordenadas máximas de la AABB

uniform vec3 u_cameraPosition;          // Posición de la cámara en el espacio mundial

in vec3 v_position;
out vec4 FragColor;                   // Color de salida del fragmento

// Función para calcular la intersección con un AABB
vec2 intersectAABB(vec3 rayOrigin, vec3 rayDir, vec3 boxMin, vec3 boxMax) {
    vec3 tMin = (boxMin - rayOrigin) / rayDir;
    vec3 tMax = (boxMax - rayOrigin) / rayDir;
    vec3 t1 = min(tMin, tMax);
    vec3 t2 = max(tMin, tMax);
    float tNear = max(max(t1.x, t1.y), t1.z);
    float tFar = min(min(t2.x, t2.y), t2.z);
    return vec2(tNear, tFar);
}

void main()
{   // Inicializar el rayo
    vec3 rayOrigin = u_cameraPosition;
    vec3 rayDirection = normalize(v_position - rayOrigin);

    // Calcular la distancia de intersección usando la función AABB
    vec2 intersection = intersectAABB(rayOrigin, rayDirection, u_boxMin, u_boxMax);

    // Calcular el espesor óptico
    float opticalThickness = (intersection.y - intersection.x);

    // Calcular el color final del píxel
    FragColor = u_backgroundColor*exp((-opticalThickness)*u_absorptionCoefficient);
}

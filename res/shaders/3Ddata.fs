uniform sampler3D volumeTexture;
uniform float densityScale;

// Ejemplo de cómo usarlo en el shader
vec4 color = texture(volumeTexture, samplePosition) * densityScale;
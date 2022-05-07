#version 460 core
out vec4 FragColor;

in vec3 outColor;
in vec2 outTexCoord;

uniform float opacity;
uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
    FragColor = mix(texture(texture1, outTexCoord), texture(texture2, outTexCoord), opacity);
} 
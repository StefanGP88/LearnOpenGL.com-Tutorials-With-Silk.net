#version 460 core
out vec4 FragColor;

in vec3 outColor;
in vec2 outTexCoord;

uniform float opacity;
uniform sampler2D texture1;

void main()
{
    FragColor = texture(texture1, outTexCoord) * vec4(outColor, opacity);
} 
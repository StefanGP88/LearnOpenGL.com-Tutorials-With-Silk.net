#version 460 core
out vec4 FragColor;

in vec3 outColor;

uniform float opacity;

void main()
{
    FragColor = vec4(outColor.x, outColor.y, opacity, 1.0);
} 
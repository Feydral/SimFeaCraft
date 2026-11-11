#version 450 core

in vec2 UVs;

uniform sampler2D Texture;

out vec4 Color;

void main()
{
    Color = texture(Texture, UVs);
}
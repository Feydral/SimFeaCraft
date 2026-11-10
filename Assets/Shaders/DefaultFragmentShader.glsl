#version 450 core

in vec2 UVs;

out vec4 Color;

uniform sampler2D Texture;

void main()
{
    Color = texture(Texture, UVs);
}
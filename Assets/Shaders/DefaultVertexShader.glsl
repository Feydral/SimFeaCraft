#version 450 core

layout (location = 0) in vec3 InPosition;
layout (location = 1) in vec2 InUVs;

uniform mat4 Model;
uniform mat4 View;
uniform mat4 Projection;

out vec2 UVs;

void main()
{
    gl_Position = vec4(InPosition, 1.0) * Model * View * Projection;
    UVs = InUVs;
}

﻿#version 330 core

layout (location = 0) in vec3 vPos;
layout (location = 1) in vec2 vUv;

uniform mat4 uModel; 

out vec2 fUv;

void main() {
    gl_Position = uModel * vec4(vPos, 1.0);
    fUv = vUv;
}
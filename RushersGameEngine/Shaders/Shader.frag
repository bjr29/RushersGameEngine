#version 330 core

in vec2 fUv;

uniform sampler2D uTexture0;

out vec4 color;

void main() {
    color = texture(uTexture0, fUv);
}
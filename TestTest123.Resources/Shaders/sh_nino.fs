#version 330 core

// Input from vertex shader
layout(location = 0) in vec4 v_Colour;
layout(location = 1) in vec2 v_TexCoord;

// Uniform for texture sampler
uniform sampler2D m_Texture;

// Output color
out vec4 finalColor;

void main() {
  // Sample the texture based on the interpolated texture coordinate
  vec4 textureColor = texture(m_Texture, v_TexCoord);
  
  // Combine vertex color and texture color (multiply for basic blending)
  finalColor = v_Colour * textureColor;
}
#version 330 core

layout(location = 0) in vec3 m_Position;
layout(location = 1) in vec4 m_Colour;
layout(location = 2) in vec2 m_TexCoord;

layout(location = 0) out vec4 v_Colour;
layout(location = 1) out vec2 v_TexCoord;


void main() {

  gl_Position = g_ProjMatrix * vec4(m_Position, 1.0);
  
  v_Colour = m_Colour;
  v_TexCoord = m_TexCoord;
}
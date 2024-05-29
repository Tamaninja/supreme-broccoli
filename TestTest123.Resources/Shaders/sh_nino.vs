#ifndef NINO_VS
#define NINO_VS

layout(location = 0) in vec3 m_Position;
layout(location = 1) in vec3 m_TexCoord;

layout(location = 0) out vec3 v_TexCoord;


void main()
{
    v_TexCoord = m_TexCoord;
    gl_Position = g_ProjMatrix * vec4(m_Position, 1.0);
}

#endif
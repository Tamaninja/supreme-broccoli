#ifndef NINO_VS
#define NINO_VS

layout(location = 0) in highp vec3 m_Position;
layout(location = 1) in lowp vec4 m_Colour;
layout(location = 2) in highp vec3 m_TexCoord;

layout(location = 0) out highp vec3 v_TexCoord;
layout(location = 1) out lowp vec4 v_Colour;

uniform mat4 g_ProjMatrix;

void main(void)
{
    v_Colour = m_Colour;
    v_TexCoord = m_TexCoord;
    gl_Position = g_ProjMatrix * vec4(m_Position, 1.0);
}

#endif
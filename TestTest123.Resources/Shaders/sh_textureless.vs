#ifndef TEXTURELESS_VS
#define TEXTURELESS_VS

layout(location = 0) in highp vec3 m_Position;
layout(location = 1) in lowp vec4 m_Colour;

layout(location = 0) out lowp vec4 v_Colour;


void main(void)
{

    v_Colour = m_Colour;
    gl_Position = g_ProjMatrix * vec4(m_Position, 1.0);
}

#endif
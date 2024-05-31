#ifndef TEXTURELESS_VS
#define TEXTURELESS_VS

layout(location = 0) in vec3 m_Position;

void main(void)
{

    gl_Position = g_ProjMatrix * vec4(m_Position, 1.0);
}

#endif
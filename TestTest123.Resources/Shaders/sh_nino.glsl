#ifndef NINO_FS
#define NINO_FS

layout(set = 0, binding = 0) uniform highp texture2D m_Texture;
layout(set = 0, binding = 1) uniform highp sampler m_Sampler;

layout(location = 0) in highp vec2 v_TexCoord;
layout(location = 1) in lowp vec4 v_Color;

layout(location = 0) out highp vec4 o_Colour;

void main(void) 
{
    o_Colour = texture(sampler2D(m_Texture, m_Sampler), v_TexCoord);
}

#endif
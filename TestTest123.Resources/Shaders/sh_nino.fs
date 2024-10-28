#ifndef NINO_FS
#define NINO_FS

layout(location = 0) in vec3 v_TexCoord;


layout(set = 0, binding = 0) uniform lowp texture2D m_Texture;
layout(set = 0, binding = 1) uniform lowp sampler m_Sampler;

layout(std140, set = 1, binding = 0) uniform u_Colour
{
    lowp vec4 color;
};

layout(location = 0) out vec4 o_Colour;



void main() 
{

    o_Colour = texture(sampler2D(m_Texture, m_Sampler), v_TexCoord.xy);
}

#endif
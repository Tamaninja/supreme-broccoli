#ifndef TEXTURELESS_FS
#define TEXTURELESS_FS

layout(location = 0) in vec4 v_Color;

layout(location = 0) out vec4 o_Colour;

layout(std140, set = 0, binding = 0) uniform u_Colour
{
    mediump vec4 color;
};


void main(void) 
{
    o_Colour = v_Color;

}

#endif

#ifndef TEXTURELESS_FS
#define TEXTURELESS_FS

layout(location = 0) in vec4 v_Color;

layout(location = 0) out vec4 o_Colour;

void main(void) 
{
    o_Colour = v_Color;
}

#endif

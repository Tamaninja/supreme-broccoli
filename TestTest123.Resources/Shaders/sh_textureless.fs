#ifndef TEXTURELESS_FS
#define TEXTURELESS_FS



layout(std140, set = 0, binding = 0) uniform u_Colour
{
    lowp vec4 color;
};

layout(location = 0) out vec4 o_Colour;


void main(void) 
{
    o_Colour = color;

}

#endif

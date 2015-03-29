#version 330

uniform vec2 in_resolution;
uniform float in_time;
uniform float in_offset;

uniform sampler2D in_tex;

void main(void)
{
    vec2 uv = gl_FragCoord.xy / in_resolution.xy;
    vec4 p = texture2D(in_tex, uv);
    
    const float mask = 0.9; // how much color is going to carry over when it is turned into a scanline
    const float sz = 3.0; // scanline height in pixels
    
    float offset = 0.0111; // sin(in_time * 10.0) / 100.0; // 3d displacement offset

    float red =   texture2D(in_tex, vec2(uv.x + offset - 0.01, uv.y)).r;
    float green = texture2D(in_tex, vec2(uv.x,                 uv.y)).g;
    float blue =  texture2D(in_tex, vec2(uv.x - offset + 0.01, uv.y)).b; 
    p = vec4(red, green, blue, 1.0);
    
    // get the row and check if the area is a scanline
    float row = floor((gl_FragCoord.y / sz)); //+ (sin(in_time * 80.0))) / sz);

    if (mod(row, 2.0) != 0.0) {
        p = vec4(p.r * mask, p.g * mask, p.b * mask, p.a * mask);
    }
    
    gl_FragColor = p;
}
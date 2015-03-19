vec4 DecodePalette(int n)
{
	return float(n > 0) * vec4(1.0, 1.0, 1.0, 0.0);
}

int ExtractBit(inout int data)
{	
	int result = int(mod(float(data), 2.0));
	data /= 2;
	
	return result;
}

bool Rect(vec2 p, vec2 topLeft, vec2 bottomRight)
{
	return 
		(p.x >= topLeft.x && p.y >= topLeft.y) &&
		(p.x <= bottomRight.x && p.y <= bottomRight.y);
		
}

vec4 DrawData(vec2 p, int data, vec2 origin, vec2 pixelSize, float mirror)
{
	vec4 pixelValue = vec4(0.0);
	int dataCopy = data;
	int cutOff = 0;
	float x = origin.x;
	for (int cutOff = 0; cutOff < 10; cutOff++)
	{
		int color = ExtractBit(dataCopy);
		
		pixelValue += 
			float(Rect(p, vec2(x, origin.y), vec2(x, origin.y) + pixelSize)) *
			DecodePalette(color);
		
		x += mirror * pixelSize.x;
		
		if (dataCopy <= 0)
		{
			return pixelValue;
		}
				  
	}
	return pixelValue;
}

int GetRowByTime(int offset)
{
	int index = int(mod(floor(iGlobalTime * 10.0 + float(offset)), 9.0));
	int result = 0;
	
	result += int(index == 8) * 8;
	result += int(index == 7) * 4;
	result += int(index == 6) * 15;
	result += int(index == 5) * 27;
	result += int(index == 4) * 63;
	result += int(index == 3) * 47;
	result += int(index == 2) * 40;
	result += int(index == 1) * 6;
	result += int(index == 0) * 0;
	
	return result;
}

vec4 Tile(vec2 p, vec2 origin, float mirror)
{
	vec4 result = vec4(0.0);
	result += DrawData(p, GetRowByTime(0), origin + vec2(0.0), vec2(16.0), mirror);
	result += DrawData(p, GetRowByTime(1), origin + vec2(0.0, 16.0), vec2(16.0), mirror);
	result += DrawData(p, GetRowByTime(2), origin + vec2(0.0, 32.0), vec2(16.0), mirror);
	result += DrawData(p, GetRowByTime(3), origin + vec2(0.0, 48.0), vec2(16.0), mirror);
	result += DrawData(p, GetRowByTime(4), origin + vec2(0.0, 64.0), vec2(16.0), mirror);
	result += DrawData(p, GetRowByTime(5), origin + vec2(0.0, 80.0), vec2(16.0), mirror);
	result += DrawData(p, GetRowByTime(6), origin + vec2(0.0, 96.0), vec2(16.0), mirror);
	result += DrawData(p, GetRowByTime(7), origin + vec2(0.0, 112.0), vec2(16.0), mirror);
	result += DrawData(p, GetRowByTime(8), origin + vec2(0.0, 128.0), vec2(16.0), mirror);
	return result;
}


void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy;
	
	
	fragColor = 
		Tile(mod(uv, vec2(192.0 + 16.0, 144.0)), vec2(96.0, 0.0), 1.0) +
		Tile(mod(uv, vec2(192.0 + 16.0, 144.0)), vec2(96.0, 0.0), -1.0);
}
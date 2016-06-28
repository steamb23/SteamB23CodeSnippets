# SteamB23.Security (C#)
메모리 해킹을 통한 값 수정을 방지하는 기능을 제공하는 프로젝트입니다.

정수 형식을 암호화하는 SecureInt64 구조체와 부동소수점 형식을 암호화하는 SecureDouble 구조체만을 지원하며 이외의 형식에 대한 암호화 구조체는 두 구조체를 통해 사용이 가능하므로 구현되어있지 않습니다.
모든 암호화 형식은 원본 형식처럼 사용할 수 있습니다.
``` c#
SecureInt64 a = 1000;
long b = a + 500;

b == 1500;
```

## MIT License
Copyright (c) 2016 SteamB23


Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
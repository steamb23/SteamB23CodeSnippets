# SteamB23.Hangul.Josa (C#)
한국어의 조사를 마지막에 오는 종성에 따라 자동으로 결정해주는 기능을 제공하는 프로젝트입니다.

포맷 문자열에서 '{인자 번호:받침조사/무받침조사}' 와 같이 사용할 수 있습니다.

이 프로젝트는 Unity3D의 런타임에서 돌아갈 수 있도록 재구성하였습니다.

## 사용 예
### Format String
```C#
// Josa josa = new Josa();
Josa josa = Josa.Instance;
string formatString = @"{0:은/는} {1:이/}다.";

// "서울은 대도시다."
josa.Process(formatString, "서울", "대도시");
// "사과는 과일이다."
josa.Process(formatString, "사과", "과일");
```


## MIT License
Copyright (c) 2016 SteamB23


Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
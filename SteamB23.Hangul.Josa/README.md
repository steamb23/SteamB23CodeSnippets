# SteamB23.Hangul.Josa (C#)
한국어의 조사를 마지막에 오는 종성에 따라 자동으로 결정해주는 기능을 제공하는 프로젝트입니다.

포맷 문자열에서 '{인자 번호:받침조사/무받침조사}' 와 같이 사용하거나
C# 6.0에서 추가된 String Interpolation 기능을 활용하여 '{"단어":받침조사/무받침조사}'와 같이 사용할 수 있습니다.

이 프로젝트의 코드를 컴파일하고 실행하려면 .Net Framework 4.6이상의 프레임워크가 필요합니다.

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

### String Interpolation
```C#
// Josa josa = new Josa();
Josa josa = Josa.Instance;
string arg0 = "서울";
string arg1 = "대도시";
string arg2 = "사과";
string arg3 = "과일";

// "서울은 대도시다."
josa.Process($"{arg0:은/는} {arg1:이/"}다.);
// "사과는 과일이다."
josa.Process($"{arg2:은/는} {arg3:이/"}다.);
```


## MIT License
Copyright (c) 2016 SteamB23


Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
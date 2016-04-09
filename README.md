SteamB23's Code Snippets
========================
이 프로젝트들은 직접 컴파일하여 쓰거나 작업중인 프로젝트에 포함시켜 사용하는 코드조각의 모음입니다.

## 하위 프로젝트

### 보안
이 프로젝트는 메모리 해킹을 통한 값 수정을 방지하는 기능을 포함하고 있습니다.

정수 형식을 암호화하는 SecureInt64 구조체와 부동소수점 형식을 암호화하는 SecureDouble 구조체만을 지원하며 이외의 형식에 대한 암호화 구조체는 두 구조체를 통해 사용이 가능하므로 구현되어있지 않습니다.

#### `SteamB23.Security.ISecureValueType<T>`
값을 암호화하여 저장하는 메서드를 정의합니다.

#### `SteamB23.Security.Encrypt`
값을 간단하게 암호화하는 정적 메서드를 제공하는 정적 클래스입니다.

만약 이 기능이 메모리 해킹 자동화 스크립트에 무력화 되었을 경우 keysLength와 keys를 수정하시기 바랍니다. keysLength는 반드시 keys의 요소 갯수와 같아야 합니다.

#### `SteamB23.Security.Hash`
가벼운 해쉬값을 내놓는 정적 메서드를 제공하는 정적 클래스입니다.

#### `System.SecureInt64`
간단한 암호화 기능을 포함하는 64 비트 정수 형식입니다.

`long`형처럼 사용가능합니다.

#### `System.SecureDouble`
간단한 암호화 기능을 포함하는 배정밀도 부동소수점 형식입니다.

`double`형처럼 사용가능합니다.


## License
Copyright (c) 2016 SteamB23


Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
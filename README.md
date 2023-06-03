# Markdown to HTML

마크다운 문서를 HTML 문서로 변환시키는 애저 펑션 API 앱입니다.

## 사전 준비물

- [Visual Studio](https://visualstudio.microsoft.com/ko/?WT.mc_id=dotnet-91712-juyoo) 또는 [Visual Studio Code](https://code.visualstudio.com/?WT.mc_id=dotnet-91712-juyoo)
- [.NET 6+](https://dotnet.microsoft.com/ko-kr/download/dotnet/?WT.mc_id=dotnet-91712-juyoo)
- [Azure Functions Core Tools v4+](https://learn.microsoft.com/ko-kr/azure/azure-functions/functions-run-local?WT.mc_id=dotnet-91712-juyoo)

## 시작하기

1. 이 리포지토리를 자신의 GitHub 계정으로 포크합니다.
1. 로컬 컴퓨터로 클론합니다.

### 로컬 컴퓨터에서 실행시키기

1. `func start` 명령어를 통해 로컬에서 실행시킵니다.
1. `http://localhost:7071/api/convert/md/to/html` 엔드포인트로 마크다운 문서를 보내 HTML 문서로 변환합니다.

### 애저로 배포해서 사용하기

1. 아래 순서대로 명령어를 실행시켜 전체 인프라를 구성합니다.

    ```powershell
    # On Windows
    $AZURE_ENV_NAME = "{{애저 리소스 이름}}"
    $AZURE_ENV_INFRA = "{{애저 배포 환경: dev|test|prod}}"
    ```

    ```bash
    # On MacOS/Linux
    AZURE_ENV_NAME="{{애저 리소스 이름}}"
    AZURE_ENV_INFRA="{{애저 배포 환경: dev|test|prod}}"
    ```

    ```bash
    azd auth login
    azd init -e $AZURE_ENV_NAME
    azd env set AZURE_ENV_INFRA $AZURE_ENV_INFRA
    azd up
    ```

1. 아래 순서대로 명령어를 실행시켜 애플리케이션을 배포합니다.

    ```powershell
    $GITHUB_USERNAME = "{{자신의 GitHub ID}}"
    ```

    ```bash
    GITHUB_USERNAME="{{자신의 GitHub ID}}"
    ```

    ```bash
    azd pipeline config

    gh auth login
    gh workflow run "Azure Deployment" --repo $GITHUB_USERNAME/infrastructure
    ```

### Arquitetura utilizada

- MVVM: Utilizado para separação de resposabilidade utilizando camadas.
- View: Definições da estrutura da tela.
- ViewModel: Disponibilizar a lógica de apresentação da tela.
- Model: Modelos de domínio da aplicação.

### Bibliotecas de terceiros utilizadas
- Newtonsoft.Json: Deserialização de arquivos JSON para classes em C#.
- sqlite-net-pc: Utilizado para armazenamento de dados do aplicativo.
- Prism MVVM: Framework MVVM para melhor organização do projeto.
- Prism.DryIoc.Forms: Container para injeção de dependência.

### O que poderia melhorar com mais tempo
- Redução do tamanho do aplicativo utilizando a ferramenta linker.
- Otimização em ambas as plataformas (Android e iOS), todo o desenvolvimento foi testado somente no Android.
- Criar testes unitários.

# Hackathon - Pos Tech - Grupo 12

|Alunos| E-mail|
|------|-------|
|Caio André Macedo Serralvo|caioserralvo182@gmail.com|
|Filipe Rosa da Silva|filipe331@gmail.com|
|Igor Hebling Sallowicz|igor.sallo@hotmail.com|

## 🚀 Introdução
Esse projeto foi desenvolvido com intuito de colocar em prática alguns conceitos aprendidos na Pós Tech Graduação da FIAP do curso 4NETT.

Todo o planejamento e desenvolvimento foi voltado ao ambiente Azure, sendo assim nossa arquitetura conta com softwares disponíveis nesse ambiente assim possíveis de simulá-los localmente.

## 📋 Arquitetura
Em nossa arquitetura utilizamos o GitHub como o versionamento de código com integração a Azure para publicação dos projetos pelo CI/CD.

Como pensamos em uma estrutura Azure, imaginando que a não teríamos restrições por conta de plano, mesmo ativando com o cartão de crédito o plano Basic, não foi possível aplicar todas as condições que colocamos aqui devido às restrições financeiras, tendo assim restrições de uso. Senado assim utilizamos os recursos disponibilizados dentro do crédito gratuito de 200 dólares com a conta mais básica ali do Azure. 
  
Iniciando pela nossa API temos o service que é o responsável pelo processamento de toda a nossa regra de negócio, sendo tratada nessa etapa. Assim que a regra de negócio é tratada, enviamos os dados para o Service Bus que está na Azure, onde ele ajuda a garantir que o banco não vai ser sobrecarregado, e ele controla a nossa fila de dados a persistência no banco.

Criamos as Azure Functions para cara fluxo (Agenda Médica, Consultas, Usuários, Pacientes e Médicos) no modo Trigger para ser disparada a cada registro, onde cada uma realiza sua função definida para persistência dos dados no banco de dados Postgres.

Olhando paras as functions de Agenda Médica utilizamos o Redis como um "semáforo" para realizar uma consulta mais rapida, de maneira que não temos concorrência para registrar a consulta.

Na Imagem abaixo temos o fluxo da arquitetura do projeto descrito acima, além de explicativos da utilização em um ambiente Produtivo visando também a escalabilidade e flexibilidade do processo, além dos explicativos dos requisitos não funcionais.

![Hackathon](https://github.com/user-attachments/assets/36e469b0-1d59-4e2c-8006-01aca7c21430)


## 🔧 Ambiente 
Criamos recursos dentro da Azure com uma conta criada somente para o Hackathon onde tivemos a restrição de limites de uso por limitação da conta Basic, conforme já explicado, sendo assim algumas ferramentas foram criadas como o banco de dados Posrtgres, Service Bus, API e algumas functions.

Da mesma forma dentro do CI/CD conseguimos realizar o CI por completo, porém limitados para o CD de algumas aplicações onde a API e alguns functions foram realizadas os deploys. **Abaixo o link da API na nuvem onde o acesso só estará disponível até o dia 09/02/2025 para fins de avaliação dos professores, visto que possui custo.**

- URL da API na Azure: [Swagger Hackathon - Pos Tech - Grupo 12](https://hackathonapigrupo12.azurewebsites.net/swagger/index.html)
- Print:
  ![Swagger API Nuvem](https://github.com/user-attachments/assets/94782ed0-222f-4dc8-95f7-7cec9c2a36ec)


**Ponto importante: O projeto não irá funcionar corretamente a API na Azure por conta das limitações, sendo assim a gravação do vídeo foi realizada em ambiente local estando somente na nuvem o Postgres, Redis e o Service Bus, já a API e as Functions foram executadas na máquina local.*

## 🛠  Utilizamos as tecnologias abaixo para o projeto:

- API - .Net 8.0
  - JWT 
  - FluentValidator
  - Entity FrameWork
  - BCrypt
- Azure Functions - .Net 8.0
- Service Bus - Azure
- Postgres - Azure
- Redis - Azure
- Application Insights - Azure

## Azure
  Abaixo alguns prints dos ambientes na Azure criados:

1 - ![image](https://github.com/user-attachments/assets/76042cd4-fea1-4525-b491-93f1c33cbaed)
2 - ![image](https://github.com/user-attachments/assets/ba1e512e-f67d-4704-88f1-cf7944092b54)
3 - Postgres, Redis, Service Bus e API ![Postgres, Redis, Service Bus e API](https://github.com/user-attachments/assets/d640863a-dd16-4542-9294-50010c34b9e1)
4 - ![image](https://github.com/user-attachments/assets/f7990a0b-8dd9-4a8a-934d-7e2c1b17a634)



## 🚀Utilizar o Sistema 🚀
A utilização do sistema foi descrito no vídeo de entrega disponível no [Link Projeto Youtube](https://youtu.be/u_Wk7_QFEv0)


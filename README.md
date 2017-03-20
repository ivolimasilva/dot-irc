# dot-irc

## Cenário
Pretende-se desenvolver um sistema de IRC (internet relay chat) baseado em .NET Remoting e com interface gráfica (GUI) nos clientes, usando .NET Windows.Forms. Cada utilizador está previamente registado num servidor comum, sendo conhecido o seu ‘nick name’ único (uma só palavra), nome real e uma ‘password’. Cada utilizador usa uma aplicação cliente que é a mesma para todos os utilizadores.

## Operação
A primeira operação a efetuar pelo utilizador, logo após pôr em execução a aplicação cliente, é um login no servidor, através do nick name e password (ou eventualmente um registo, se não existir esse utilizador). Após um login com sucesso o servidor pode fornecer ao cliente a lista de utilizadores no estado ativo (com login efetuado). Essa lista deve manter-se atualizada em tempo real, ou seja, sempre que surja um novo utilizador, os outros devem vê-lo imediatamente, e a mesma coisa quando um utilizador executa um logout (ou simplesmente fecha a janela do cliente).

Após o login e com a lista de utilizadores ativos preenchida, o utilizador pode escolher um e solicitar um chat. Se for aceite pelo outro utilizador deverá ser criada uma nova janela para o chat, (em cada lado) contendo pelo menos uma linha editável para envio de mensagens, um botão de envio e uma caixa de texto com as mensagens enviadas e recebidas, devidamente identificadas (por exemplo, usando cores diferentes).

Qualquer utilizador poderá terminar o chat fechando essa janela (o que deve levar ao fecho da correspondente, no outro lado da comunicação).

A sessão é terminada com um logout na janela inicial, ou simplesmente fechando-a.

![System Architecture](http://i.imgur.com/QGdAeBb.png)

## Interações
As operações de login, logout (e eventualmente de registo de um novo utilizador) são levadas a cabo entre cada cliente e o servidor único. Este, sempre que há qualquer alteração no estado de login dos clientes, deve notificar todos os outros de modo a atualizarem as suas listas de utilizadores ativos.

As conversações devem fazer-se **diretamente** entre os clientes. Para isso o servidor indica um "endereço" suficiente de cada cliente, quando fornece ou atualiza a lista com os clientes ativos. Havendo vários clientes na mesma máquina deverão ter os seus objetos remotos em portos diferentes, atribuídos automaticamente quando do arranque da aplicação.

O servidor deve persistir a informação dos utilizadores registados.

## Implementação
Deverá implementar um demonstrador deste projeto usando .NET Remoting e desenvolvendo as aplicações necessárias, utilizando as suas boas práticas. As aplicações devem ter uma interface intuitiva e fácil de usar. Poderá adicionar outras funcionalidades que considere convenientes, por exemplo permitir que cada utilizador mantenha chats com vários parceiros simultaneamente (em janelas separadas), possa partilhar outro tipo de informação que não texto, seja possível criar chat rooms entre vários utilizadores, ...

## Relatório
Deverá ser entregue um pequeno relatório descrevendo a arquitetura usada (aplicações, módulos, objetos remotos, eventos remotos e sua interação), funcionalidades incluídas, testes executados, e modo de funcionamento (capturas dos principais fluxos de utilização).

Deverá conter também todas as instruções para construir e usar todo o demonstrador.
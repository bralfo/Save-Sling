# Save Sling

Save Sling é um jogo 2D desenvolvido em Unity e C# para uma Game Jam com o tema **Salvage**.

O jogador está no topo de um prédio em chamas e precisa ajudar a resgatar objetos valiosos. NPCs chegam carregando diferentes itens e os colocam em um estilingue. O jogador deve mirar e lançar cada objeto até uma área segura antes que o tempo acabe.

Cada objeto possui características próprias, como valor e peso, fazendo com que o jogador precise considerar a trajetória de cada lançamento.

## Gameplay

O ciclo principal do jogo funciona da seguinte forma:

1. Um NPC chega ao prédio carregando um item.
2. Os NPCs aguardam em uma fila.
3. O primeiro NPC entrega seu item ao estilingue.
4. O jogador mira utilizando o mouse.
5. O jogador puxa e solta o estilingue para lançar o objeto.
6. A câmera acompanha o objeto durante o lançamento.
7. Se o objeto chegar à Safe Zone, ele é resgatado e seu valor é adicionado à pontuação.
8. Se o objeto cair fora da área segura, ele é perdido.
9. O NPC que entregou o item deixa o prédio.
10. O próximo NPC avança na fila.

O jogador precisa repetir esse processo enquanto administra o tempo disponível.

## Sistemas desenvolvidos

### Estilingue

Sistema de lançamento baseado em física 2D.

* Sistema de mira utilizando o mouse.
* Limite de distância ao puxar o estilingue.
* Aplicação de força utilizando `Rigidbody2D`.
* Gravidade e trajetória física dos objetos.
* Efeitos sonoros durante o lançamento.

### NPC System

Sistema responsável pelo fluxo dos NPCs.

* Spawn automático de NPCs.
* Sistema de fila.
* Avanço automático dos NPCs.
* Seleção aleatória de personagens.
* Animações dos NPCs.
* Entrega de itens ao estilingue.
* Saída dos NPCs após o lançamento do item.

### Salvage Items

Os objetos resgatáveis possuem propriedades próprias:

* Nome.
* Valor.
* Peso.
* Mensagem narrativa.
* Física individual baseada no peso.

O peso do objeto é aplicado diretamente ao `Rigidbody2D`, permitindo que diferentes itens tenham comportamentos físicos distintos durante o lançamento.

### Safe Zone

A Safe Zone verifica os objetos que chegam à área de resgate.

Quando um item válido entra na área:

* O item é identificado.
* Seu valor é adicionado à carteira.
* Uma mensagem relacionada ao item pode ser exibida.
* A câmera retorna ao estilingue.
* O objeto é removido da cena.

### Sistema de pontuação

O jogo possui um sistema de economia baseado em ouro.

A pontuação é atualizada através de eventos e exibida na interface utilizando TextMesh Pro.

O progresso também utiliza `PlayerPrefs` para armazenar o ouro acumulado entre partidas.

### Sistema de tempo

As partidas possuem um limite de tempo.

Quando o tempo chega a zero:

* A partida é encerrada.
* O ouro obtido na rodada é processado.
* O progresso é salvo.
* O Game Over é acionado.

### Câmera dinâmica

Durante o lançamento, a câmera acompanha o objeto para permitir que o jogador acompanhe sua trajetória.

Após o objeto chegar ao destino ou ser perdido, a câmera retorna ao estilingue.

## Tecnologias

* Unity
* C#
* Unity 2D Physics
* Unity Input System
* TextMesh Pro
* Git / GitHub

## Arquitetura

Os scripts foram organizados por sistemas para facilitar a manutenção e evolução do projeto:

```text
Assets/
└── Scripts/
    ├── Bufunfa/
    ├── Camera/
    ├── Estilingue/
    ├── GameManager/
    ├── Items/
    ├── Menu/
    ├── NPC/
    ├── SafeZone/
    ├── Time/
    └── UI/
```

Alguns dos principais componentes são:

```text
GameManager
    └── Controle geral da partida

NPCSpawner
    └── Spawn e gerenciamento da fila de NPCs

NPC
    └── Movimento, fila e entrega de itens

Estilingue
    └── Mira e lançamento dos objetos

SalvageItem
    └── Dados e comportamento dos itens

SafeZone
    └── Validação dos itens resgatados

Wallet
    └── Gerenciamento do ouro

LevelTimer
    └── Controle do tempo da partida
```

## O que desenvolvi

Neste projeto trabalhei principalmente com **programação de gameplay e implementação de sistemas em Unity**, incluindo:

* Desenvolvimento da mecânica principal de lançamento.
* Programação dos NPCs e sistema de filas.
* Sistema de spawning.
* Implementação da física dos objetos.
* Sistema de resgate e pontuação.
* Gerenciamento do tempo da partida.
* Integração entre gameplay, câmera e interface.
* Organização dos scripts em sistemas separados.
* Prototipação e implementação das mecânicas durante a Game Jam.

## Screenshots

Adicione aqui screenshots do jogo:

![Gameplay](Assets/Images/gameplay.png)

## Gameplay Video

Adicione aqui um vídeo demonstrando o jogo.

## Download

[Download do jogo](../../releases)

### Brendon Alexander

As Game Developer and Game Designer, I worked on the core gameplay and systems of Save Sling.

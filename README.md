# Pinpad Sdk

Oferece métodos para a comunicação com o pinpad através do protocolo ABECS.

## Dependências

Unica dependência:
- MicroPos.CrossPlatform: conhece a implementação da plataforma em si. É necessária, porque a porta serial é conhecida através da plataforma.

## Como usar?

A classe principal do pinpad é a **IPinpadFacade**. Ela possui adapters para todos os "componentes" do pinpad, que são:

Componente | Descrição | Funcionalidade
--- | --- | ---
PinpadConnection | Controlar a conexão física da porta serial. Consequentemente, ela conhece a [MicroPos.CrossPlatform](https://bitbucket.org/stonepayments/micropos-crossplatform), que possui classes de acesso a plataforma em si. | Abrir conexão, fechar conexão, verificar se a conexão está ativa.
PinpadCommunication | Conhecer os comandos que serão enviados ao pinpad. | Enviar comandos ao pinpad, receber comandos do pinpad.
PinpadTransaction | Utilizar a PinpadCommunication para performar os comandos necessários em um fluxo transacional (fazer o download de tabelas, ler o cartão e a senha). | Atualizar tabelas do pinpad, ler o cartão, ler a senha, pedir para remover o cartão, finalizar transação EMV.
IPinpadKeyboard | Dar acesso ao teclado do pinpad. | Ler uma tecla de controle, ler um input (apenas MobiPin10).
IPinpadDisplay | Dar acesso ao display do pinpad. | Mostrar uma frase na tela do pinpad.
IPinpadInfos | Dar acesso às informações do pinpad. | Retornar todas as informações relativas ao pinpad e à fabricante do pinpad.

### Contextos

Cada fabricante de pinpad possui sua implementação do protocolo ABECS, chamada de Biblioteca Compartilhada. Cada fabricante pode possuir comandos personalizados que não estão no protocolo ABECS, mas que estão na BC. Dessa forma, em dependendo do pinpad que for usado, existem dois tipos de comandos: **os da ABECS e os proprietários, que variam de acordo com a fabricante**.

Tendo isso em vista, a SDK possui classes de contexto. Para cada nova fabricante suportada, um novo contexto é adicionado. Esse contexto define como os comandos proprietários deverão ser usados (byte de início, byte de final, checksum ou byte de validação do pacote, etc.).

### Consumindo a IPinpadFacade

Cada IPinpadFacade está associada a um pinpad, ou seja, à uma conexão física. Por isso, uma conexão tem que ser aberta e passada como parâmetro na donstrução da PinpadFacade.

Isso pode ser feito de dois jeitos:

- Quando você conhece a porta seriar na qual o pinpad está conectado:

```
// Faz a conexão
IPinpadConnection connection = PinpadConnectionManager.PinpadConnectionController.CreatePinpadConnection(portName);

if (connection != null)
{
    // Abre conexão e cria o facade:
    connection.Open();
    IPinpadFacade facade = new PinpadFacade(connection);
}
```

- Quando não se sabe em que porta o pinpad está conectado:

```
// Pega o primeiro pinpad detectado na máquina:
IPinpadConnection connection = PinpadConnection.GetFirst();

if (connection != null)
{
    // Cria facade para o pinpad conectado:
    IPinpadFacade facade = new PinpadFacade(connection);
}
```

### Verificar se o pinpad está conectado

```
if (facade.Connection.Ping() == true)
{
    // Conexão está ativa.
}
```

### Ler um cartão

```
TransactionType transactionType = TransactionType.Undefined;

try 
{
    // Não conhece o tipo da transação. Nesse caso, o tipo da transação selecionado pelo pinpad
    // será atribuído à variável transactionType.
    CardEntry myCard = facade.TransactionService.ReadCard(TransactionType.Undefined, 1.99m, out transactionType);
}
catch (CardHasChipException chce)
{
    // Um cartão de chip foi passado como tarja
}
catch (ExpiredCardException ece)
{
    // Um cartão com data de validade expirada foi passado
}
```

### Mostrar mensagem na tela do pinpad

```
facade.Display.ShowMessage("Olá", "Tudo bem?", DisplayPaddingType.Center);
```

### Ler tecla de comando

Teclas de comando são: **enter, clear, teclas de função, cancel**.

```
// Lê a tecla pressionada
PinpadKeyCode keyPressed = facade.Keyboard.GetKey();

// Trata retorno
if (key == PinpadKeyCode.Return)
{
    // Pressionou enter
}
else if (key == PinpadKeyCode.Cancel)
{
    // Pressionou cancel
}
```

### Ler input numerico do teclado

> ** :warning: WARNING: ** Apenas suportado no MOBIPIN10 da Gertec.

```
short minimumDigits = 8;
short maxDigits = 16;
short timeOut = 120;

// Lê um numero de telefone com no mínimo 8 caracteres e 16 no máximo, com timeout de 2 minutos.
string telephoneNumber = facade.Keyboard.GetNumericInput(FirstLineLabelCode.Type, SecondLineLabelCode.TelephoneNumber,
    minimumDigits, maxDigits, timeOut);
```

# Dúvidas? :octopus:

Fala com a gente! [:green_heart: DEV MicroTef](mailto:devmicrotef@stone.com.br)

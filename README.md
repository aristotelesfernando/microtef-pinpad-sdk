# Pinpad Sdk

Oferece métodos para a comunicação com o pinpad através do protocolo ABECS.

## Dependências

Unica dependência:
- MicroPos.CrossPlatform: conhece a implementação da plataforma em si. É necessária, porque a porta serial é conhecida através da plataforma.

## Como usar?

A classe principal do pinpad é a **IPinpadFacade**. Ela possui adapters para todos os "componentes" do pinpad, que são:

Componente | Função
--- | ---
PinpadConnection | Controlar a conexão física da porta serial. Consequentemente, ela conhece a [MicroPos.CrossPlatform](https://bitbucket.org/stonepayments/micropos-crossplatform), que possui classes de acesso a plataforma em si.
PinpadCommunication | Conhecer os comandos que serão enviados ao pinpad.
PinpadTransaction | Utilizar a PinpadCommunication para performar os comandos necessários em um fluxo transacional (fazer o download de tabelas, ler o cartão e a senha).
IPinpadKeyboard | Dar acesso ao teclado do pinpad.
IPinpadDisplay | Dar acesso ao display do pinpad.
IPinpadInfos | Dar acesso às informações do pinpad.

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

## Duvidas? Entre em contato: [devmicrotef@stone.com.br](mailto:devmicrotef@stone.com.br) :octopus:

> ## Responsáveis imediatos
- Ceres Rohana: [ccarvalho@stone.com.br](mailto:ccarvalho@stone.com.br)
- Cristina Silva: [ccsilva@stone.com.br](mailto:ccsilva@stone.com.br)
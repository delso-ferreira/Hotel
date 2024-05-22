<h1>HotelBookingAPI 🏨 🔄 🌐</h1>

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/delso-ferreira/Hotel/blob/main/LICENSE)

<h2>Descrição</h2>

A **HotelBookingAPI** é uma aplicação desenvolvida em C# dentro do curso de desenvolvimento web da **Trybe**. Foi utilizada a arquitetura MVC que interage com um banco de dados MySQL. Esta API permite que os usuários busquem hotéis próximos à sua localização, façam reservas de hotéis e quartos, e realizem operações CRUD (Criação, Busca, Alteração e Exclusão) sobre os dados armazenados. Para garantir a segurança e controle de acesso, a API utiliza Policy e Claims, juntamente com JWT (JSON Web Token).

<h2>Funcionalidades</h2>	

**Busca de Hotéis:** Pesquise hotéis próximos à localização do usuário dentro de um raio específico em quilômetros.

**Reservas:** Realize reservas de hotéis e quartos disponíveis no banco de dados.

**Segurança:** Implementação de JWT para autenticação e uso de Policy e Claims para controle de acesso.


<h2>Tecnologias Utilizadas</h2>

✔️  C#
✔️  ASP.NET Core MVC
✔️  MySQL
✔️  Entity Framework
✔️  Pomelo
✔️  JWT (JSON Web Token)

<h2>Requisitos</h2>

.NET 6 SDK
Servidor MySQL
Ferramenta de gerenciamento de banco de dados (e.g., MySQL Workbench)

<h2>Instalação</h2>
Clone o repositório:

```
git clone https://github.com/seuusuario/HotelBookingAPI.git
cd HotelBookingAPI
```

Instale as dependências:

```
dotnet restore
```

Aplicar as migrações do banco de dados:

```
dotnet ef database update
```

Iniciar a aplicação:

```
dotnet run
```

<h2>Uso</h2>

A aplicação está disponível publicamente em: [HotelBookingAPI](https://hotel-production-9bb8.up.railway.app/)

*Exemplos:*

***Buscar hóteis***

Requisição:
```
GET api/hotel
```

Resposta:
```
[
  {
    "hotelId": 1,
    "name": "Copacabana Palace",
    "address": "Av. Atlântica, 1702 - Copacabana, Rio de Janeiro",
    "cityId": 1,
    "cityName": "Rio de Janeiro",
    "state": "RJ"
  },
  {
    "hotelId": 2,
    "name": "Hotel Fasano",
    "address": "Av. Vieira Souto, 80 - Ipanema, Rio de Janeiro",
    "cityId": 1,
    "cityName": "Rio de Janeiro",
    "state": "RJ"
  },
  ...
]
```

***Buscar Cidades***

Requisição:
```
GET api/city
```

Resposta:
```
[
  {
    "cityId": 1,
    "name": "Rio de Janeiro",
    "state": "RJ"
  },
  {
    "cityId": 2,
    "name": "São Paulo",
    "state": "SP"
  },
  ...
]
```

***Reservar Hotel:***

Endpoint:
```
POST api/booking
```

Requisição:
```
{
	"CheckIn":"2030-08-27",
	"CheckOut":"2030-08-28",
	"GuestQuant":"1",
	"RoomId":1
}
```

Resposta:

```
"bookingId": 1,
	"checkIn": "2030-08-27T00:00:00",
	"checkOut": "2030-08-28T00:00:00",
	"guestQuant": 1,
	"room": {
		"roomId": 1,
		"name": "Quarto Deluxe",
		"capacity": 2,
		"image": "image",
		"hotel": {
			"hotelId": 1,
			"name": "Copacabana Palace",
			"address": "Av. Atlântica, 1702 - Copacabana, Rio de Janeiro",
			"cityId": 1,
			"cityName": "Rio de Janeiro"
		}
	}
```


Adicionar Hotel:

Endpoint:
```
POST api/hotel
```

Requisição:
```
{
	"Name":"Hotel Novo",
	"Address":"Avenida Atlântica, 1400",
	"CityId": 1
}
```


Resposta: 
```
{
	"hotelId": 2,
  	"name": "Hotel Novo",
  	"address": "Avenida Atlântica, 1400",
  	"cityId": 1,
  	"cityName": "Rio de Janeiro",
    "state": "RJ"
}
```

<h2>Autenticação e Autorização</h2>

A API utiliza JWT para autenticação. Para acessar endpoints protegidos, o usuário deve incluir um token JWT válido no 

Obter Token JWT:

Endpoint:
```
POST /api/login
```

Requisição:
```
{
	"Email": "nome.sobrenome@hotelbookingapi.com",
	"Password": "123456"
}
```


Resposta:
```
{
"token": token
}
```

Utilize o token recebido para acessar endpoints protegidos adicionando-o ao cabeçalho Authorization


<h2>Contribuição</h2>

Sinta-se à vontade para contribuir com o projeto. Para isso, siga os passos abaixo:

***Fork o repositório***

Crie uma branch para sua feature (git checkout -b feature/nome-da-feature)
Faça commit das suas alterações (git commit -m 'feat:Adicionar nova feature')
Envie para a branch (git push origin feature/nome-da-feature)
Abra um Pull Request

##Licença

Este projeto está licenciado sob os termos da licença MIT. Veja o arquivo LICENSE para mais detalhes.


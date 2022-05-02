O projeto foi desenvolvido em 4 camadas:

- TesteTecnicoWK.API
	Camada de API a qual disponibiliza os dados para outros projetos via REST.
	A configuração do acesso ao banco de dados foi feita nesta camada dentro do aquivo appsettings.json, cofiurar a "ConnectionStrings" e depois rodar via Console 
	o comando Update-Database do Migration com o projeto de API selecionado

- TesteTecnicoWK.Data.Entity.MySQL
        Camada que possui toa a lógia de persitencia de dados.

- TesteTecnicoWK.Data.Dominio
	Camada onde foram criadas as classes modelos

- TesteTecnicoWK.WEB
        Camada de UI para manipulação dos dados, consome as informações da API via requisições REST. Para testa-la o projeto de API deve ser compulado juntamente.
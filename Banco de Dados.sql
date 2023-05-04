drop database dbVeterinario;
create database dbVeterinario;
use dbVeterinario;


create table tblLogin(
usuario varchar(50) primary key,
senha varchar(10),
tipo int
);

create table tblCliente(
codCliente int primary key auto_increment,
nomeCliente varchar(50),
telCliente varchar(20),
emailCliente varchar(50)
);

create table tblVeterinario(
codVeterinario int primary key auto_increment,
nomeVeterinario varchar(50),
telVeterinario varchar(20),
emailVeterinario varchar(50)
);

create table tblTipo(
codTipo int primary key auto_increment,
tipo varchar(50)
);

create table tblRaca(
codRaca int primary key auto_increment,
nomeRaca varchar(50),
codTipo int references tbTipo(codTipo)
);

create table tblAnimal(
codAnimal int primary key auto_increment,
nomeAnimal varchar(50),
codRaca int references tblRaca(codRaca),
codCliente int references tblCliente(codCliente)
);

create table tblAtendimento(
codAtendimento int primary key auto_increment,
dataAtendimento varchar(50),
horaAtendimento varchar(10),
statusAtendimento varchar(50),
codVeterinario int references tblVeterinario(codVeterinario),
codAnimal int references tblAnimal(codAnimal)
);

/************************************************************************************************************/

insert into tblLogin values 
("Pedro", "pedro123", 1), ("Marcus", "marcus123", 2);

/************************************************************************************************************/

 -- PROCEDURES CLIENTE
-- Procedure inserir cliente 
delimiter $$
create procedure pcd_insertCliente(
_nomeCliente varchar(50),
_telCliente varchar(20),
_emailCliente varchar(50)
)
begin
	start transaction;
		insert into tblCliente(nomeCliente, telCliente, emailCliente)
		values(_nomeCliente, _telCliente, _emailCliente);
	commit;
		rollback;
end $$

-- Procedure consultar cliente
delimiter $$
create procedure pcd_selectCliente()
begin
	select * from tblCliente;
end $$

-- Procedure alterar cliente
delimiter $$
create procedure pcd_updateCliente(
_codCliente int,
_nomeCliente varchar(50),
_telCliente varchar(20),
_emailCliente varchar(50)
)
begin
	start transaction;
		update tblCliente set 	nomeCliente = _nomeCliente, telCliente = _telCliente, 
								emailCliente = _emailCliente where codCliente = _codCliente;
	commit;
		rollback;
end $$

-- Procedure apagar cliente
delimiter $$
create procedure pcd_deleteCliente(_codCliente int)
begin
	delete from tblCliente where codCliente = _codCliente;
end $$
-- */

 -- PROCEDURES VETERINÁRIO
-- Procedure inserir veterinario 
delimiter $$
create procedure pcd_insertVeterinario(
_nomeVeterinario varchar(50),
_telVeterinario varchar(20),
_emailVeterinario varchar(50)
)
begin
	start transaction;
		insert into tblVeterinario(nomeVeterinario, telVeterinario, emailVeterinario)
		values(_nomeVeterinario, _telVeterinario, _emailVeterinario);
	commit;
		rollback;
end $$

-- Procedure consultar veterinario por código 
delimiter $$

CREATE PROCEDURE pcd_selectVeterinario_porNome(IN nomeVeterinario VARCHAR(50))
BEGIN
	SELECT * FROM tblVeterinario WHERE nomeVeterinario LIKE CONCAT('%', nomeVeterinario, '%');
END $$

-- Procedure consultar veterinario 
delimiter $$
create procedure pcd_selectVeterinario()
begin
	select * from tblVeterinario;
end $$

-- Procedure alterar veterinario 
delimiter $$
create procedure pcd_updateVeterinario(
_codVeterinario int,
_nomeVeterinario varchar(50),
_telVeterinario varchar(20),
_emailVeterinario varchar(50)
)
begin
	start transaction;
		update tblVeterinario set 	nomeVeterinario = _nomeVeterinario, telVeterinario = _telVeterinario, 
								emailVeterinario = _emailVeterinario where codVeterinario = _codVeterinario;
	commit;
		rollback;
end $$

-- Procedure apagar veterinario
delimiter $$
create procedure pcd_deleteVeterinario(_codVeterinario int)
begin
	delete from tblVeterinario where codVeterinario = _codVeterinario;
end $$
-- */

 -- PROCEDURES TIPO
-- Procedure inserir tipo 
delimiter $$
create procedure pcd_insertTipo(
_tipo varchar(50)
)
begin
	start transaction;
		insert into tblTipo(tipo)
		values(_tipo);
	commit;
		rollback;
end $$

-- Procedure consultar tipo 
delimiter $$
create procedure pcd_selectTipo()
begin
	select * from tblTipo;
end $$

-- Procedure alterar tipo 
delimiter $$
create procedure pcd_updateTipo(
_codTipo int,
_tipo varchar(50)
)
begin
	start transaction;
		update tblTipo set tipo = _tipo where codTipo = _codTipo;
	commit;
		rollback;
end $$

-- Procedure apagar tipo
delimiter $$
create procedure pcd_deleteTipo(_codTipo int)
begin
	delete from tblTipo where codTipo = _codTipo;
end $$
-- */

 -- PROCEDURES RAÇA
-- Procedure inserir raca 
delimiter $$
create procedure pcd_insertRaca(
_nomeRaca varchar(50),
_codTipo int
)
begin
	start transaction;
		insert into tblRaca(nomeRaca, codTipo)
		values(_nomeRaca, _codTipo);
	commit;
		rollback;
end $$

-- Procedure consultar raca 
delimiter $$
create procedure pcd_selectRaca()
begin
	select 
	 t1.codRaca,
	 t1.nomeRaca, 
	 t2.tipo from tblRaca as t1 
	 INNER JOIN tblTipo as t2 ON t1.codTipo = t2.codTipo;
end $$

-- Procedure alterar raca 
delimiter $$
create procedure pcd_updateRaca(
_codRaca int,
_nomeRaca varchar(50),
_codTipo int
)
begin
	start transaction;
		update tblRaca set nomeRaca = _nomeRaca, codTipo = _codTipo 
		where codRaca = _codRaca;
	commit;
		rollback;
end $$

-- Procedure apagar raca
delimiter $$
create procedure pcd_deleteRaca(_codRaca int)
begin
	delete from tblRaca where codRaca = _codRaca;
end $$
-- */

 -- PROCEDURES ANIMAL
-- Procedure inserir animal 
delimiter $$
create procedure pcd_insertAnimal(
_nomeAnimal varchar(50),
_codRaca int,
_codCliente int
)
begin
	start transaction;
		insert into tblAnimal(nomeAnimal, codRaca, codCliente)
		values(_nomeAnimal, _codRaca, _codCliente);
	commit;
		rollback;
end $$

-- Procedure consultar animal por código 
delimiter $$
create procedure pcd_selectAnimal_porNome(_nomeAnimal VARCHAR(50))
begin
	select 
		t1.codAnimal,
		t1.nomeAnimal, 
		t2.nomeRaca,
		t3.nomeCliente from tblAnimal as t1 
		INNER JOIN tblRaca as t2 ON t1.codRaca = t2.codRaca
		INNER JOIN tblCliente as t3 ON t1.codCliente = t3.codCliente
	 where nomeAnimal like concat('%', _nomeAnimal, '%');
end $$

-- Procedure consultar animal 
delimiter $$
create procedure pcd_selectAnimal()
begin
	select 
		t1.codAnimal,
        t1.nomeAnimal, 
        t2.nomeRaca,
        t3.nomeCliente from tblAnimal as t1 
        INNER JOIN tblRaca as t2 ON t1.codRaca = t2.codRaca
        INNER JOIN tblCliente as t3 ON t1.codCliente = t3.codCliente;
end $$

-- Procedure alterar animal 
delimiter $$
create procedure pcd_updateAnimal(
_codAnimal int,
_nomeAnimal varchar(50),
_codRaca int,
_codCliente int
)
begin
	start transaction;
		update tblAnimal set nomeAnimal = _nomeAnimal, codRaca = _codRaca , codCliente = _codCliente 
		where codAnimal = _codAnimal;
	commit;
		rollback;
end $$

-- Procedure apagar animal
delimiter $$
create procedure pcd_deleteAnimal(_codAnimal int)
begin
	delete from tblAnimal where codAnimal = _codAnimal;
end $$
-- */

 -- PROCEDURES ATENDIMENTO
-- Procedure inserir atendimento 
delimiter $$
create procedure pcd_insertAtendimento(
_dataAtendimento varchar(50),
_horaAtendimento varchar(10),
_statusAtendimento varchar(50),
_codVeterinario int,
_codAnimal int
)
begin
	start transaction;
		insert into tblAtendimento(dataAtendimento, horaAtendimento, statusAtendimento, codVeterinario, codAnimal)
		values(_dataAtendimento, _horaAtendimento, _statusAtendimento, _codVeterinario, _codAnimal);
	commit;
		rollback;
end $$

-- Procedure consultar atendimento por código 
delimiter $$
create procedure pcd_selectAtendimento_porCod(_codAtendimento int)
begin
	select 
		t1.codAtendimento,
		t1.dataAtendimento, 
		t1.horaAtendimento, 
		t1.statusAtendimento, 
		t2.nomeVeterinario,
		t3.nomeAnimal from tblAtendimento as t1 
		INNER JOIN tblVeterinario as t2 ON t1.codVeterinario = t2.codVeterinario
		INNER JOIN tblAnimal as t3 ON t1.codAnimal = t3.codAnimal
	 where codAtendimento = _codAtendimento;
end $$

-- Procedure consultar atendimento 
delimiter $$
create procedure pcd_selectAtendimento()
begin
	select 
		t1.codAtendimento,
		t1.dataAtendimento, 
		t1.horaAtendimento, 
		t1.statusAtendimento, 
		t2.nomeVeterinario,
		t3.nomeAnimal from tblAtendimento as t1 
		INNER JOIN tblVeterinario as t2 ON t1.codVeterinario = t2.codVeterinario
		INNER JOIN tblAnimal as t3 ON t1.codAnimal = t3.codAnimal;
end $$

-- Procedure alterar atendimento 
delimiter $$
create procedure pcd_updateAtendimento(
_codAtendimento int,
_dataAtendimento varchar(50),
_horaAtendimento varchar(10),
_statusAtendimento varchar(50),
_codVeterinario int,
_codAnimal int
)
begin
	start transaction;
		update tblAtendimento set dataAtendimento = _dataAtendimento, horaAtendimento = _horaAtendimento, 
        statusAtendimento = _statusAtendimento, codVeterinario = _codVeterinario , codAnimal = _codAnimal 
        where codAtendimento = _codAtendimento;
	commit;
		rollback;
end $$

-- Procedure apagar atendimento
delimiter $$
create procedure pcd_deleteAtendimento(_codAtendimento int)
begin
	delete from tblAtendimento where codAtendimento = _codAtendimento;
end $$
-- */

/************************************************************************************************************/

 -- CHAMADAS PROCEDURES CLIENTE
-- Chamada da Procedure inserir cliente 
call pcd_insertCliente("Ana Silva", "(11) 98765-1234", "anasilva@email.com");
call pcd_insertCliente("Bruno Almeida", "(21) 99876-2345", "brunoalmeida@email.com");
call pcd_insertCliente("Carla Oliveira", "(51) 98765-3456", "carlaoliveira@email.com");
call pcd_insertCliente("Daniel Santos", "(31) 99876-4567", "danielsantos@email.com");


-- Chamada da Procedure consultar cliente 
call pcd_selectCliente();
	
-- Chamada da Procedure alterar cliente
call pcd_updateCliente(3, "Emily Lima", "(81) 98765-5678", "emilyferreira@email.com");

-- Chamada da Procedure apagar cliente
-- call pcd_deleteCliente(4); 
-- */

 -- CHAMADAS PROCEDURES VETERINÁRIO
-- Chamada da Procedure inserir veterinario 
call pcd_insertVeterinario("Ana Paula Gonçalves", "(11) 98765-8765", "anapaula.vet@email.com");
call pcd_insertVeterinario("Bruno Costa", "(21) 99876-7654", "brunocosta.vet@email.com");
call pcd_insertVeterinario("Camila Souza", "(51) 98765-6543", "camilasouza.vet@email.com");
call pcd_insertVeterinario("Daniel Oliveira", "(31) 99876-5432", "danieloliveira.vet@email.com");

-- Chamada da Procedure consultar veterinario por código 
call pcd_selectVeterinario_porNome("ana");

-- Chamada da Procedure consultar veterinario 
call pcd_selectVeterinario();
	
-- Chamada da Procedure alterar veterinario 
call pcd_updateVeterinario(3, "Elisa Pereira", "(81) 98765-4321", "elisapereira.vet@email.com");

-- Chamada da Procedure apagar veterinario
-- call pcd_deleteVeterinario(4);
-- */

 -- CHAMADAS PROCEDURES TIPO
-- Chamada da Procedure inserir tipo 
call pcd_insertTipo("Cachorro");
call pcd_insertTipo("Gato");
call pcd_insertTipo("Pássaro");
call pcd_insertTipo("Hamster");

-- Chamada da Procedure consultar tipo 
call pcd_selectTipo();
	
-- Chamada da Procedure alterar tipo 
call pcd_updateTipo(4, "Cobra");

-- Chamada da Procedure apagar tipo
-- call pcd_deleteTipo(3);
-- */

 -- CHAMADAS PROCEDURES RAÇA
-- Chamada da Procedure inserir raca 
call pcd_insertRaca("Pug", 1);
call pcd_insertRaca("Siamês", 2);
call pcd_insertRaca("Papagaio", 3);
call pcd_insertRaca("Sírio ", 4);

-- Chamada da Procedure consultar raca 
call pcd_selectRaca();
	
-- Chamada da Procedure alterar raca 
call pcd_updateRaca(4, "Jiboia", 4);

-- Chamada da Procedure apagar raca
-- call pcd_deleteRaca(3);
-- */

 -- CHAMADAS PROCEDURES ANIMAL
-- Chamada da Procedure inserir animal 
call pcd_insertAnimal("Thor", 1, 1);
call pcd_insertAnimal("Anubis", 2, 2);
call pcd_insertAnimal("Charlie", 3, 3);
call pcd_insertAnimal("Simon ", 4, 4);

-- Chamada da Procedure consultar animal por código 
call pcd_selectAnimal_porNome("nu");

-- Chamada da Procedure consultar animal 
call pcd_selectAnimal();
	
-- Chamada da Procedure alterar animal 
call pcd_updateAnimal(4, "Saphira", 4, 4);

-- Chamada da Procedure apagar animal
-- call pcd_deleteAnimal(3);
-- */

 -- CHAMADAS PROCEDURES ATENDIMENTO
-- Chamada da Procedure inserir atendimento 
call pcd_insertAtendimento("25/04/2023", "10:00", "Agendado", 1, 2);
call pcd_insertAtendimento("23/04/2023", "14:30", "Atendido", 2, 3);
call pcd_insertAtendimento("24/04/2023", "09:15", "Agendado", 3, 4);
call pcd_insertAtendimento("22/04/2023", "11:45", "Atendido", 4, 1);

-- Chamada da Procedure consultar atendimento 
call pcd_selectAtendimento();
	
-- Chamada da Procedure alterar atendimento 
call pcd_updateAtendimento(5, "26/04/2023", "13:30", "Agendado", 1, 3);

-- Chamada da Procedure apagar atendimento
-- call pcd_deleteAtendimento(3);
-- */

/************************************************************************************************************/

 -- SELECT DE TODAS AS TABELAS
select * from tblLogin;
select * from tblCliente;
select * from tblVeterinario;
select * from tblTipo;
select * from tblRaca;
select * from tblAnimal;
select * from tblAtendimento;
-- */
CREATE DATABASE todo_list
GO

USE todo_list
GO

CREATE TABLE usuario
(
	ide_usr BIGINT IDENTITY(1,1) PRIMARY KEY,
	cor_usr VARCHAR(150) NOT NULL UNIQUE, -- Correo 
    pwd_usr VARCHAR(255) NOT NULL, -- Contraseña
    nom_usr VARCHAR(100) NOT NULL, -- Nombre
    ape_usr VARCHAR(100) NOT NULL, -- Apellido
	fech_registro DATETIME DEFAULT GETDATE(),
)


CREATE TABLE tareas 
(
	ide_tar BIGINT IDENTITY(1,1) PRIMARY KEY,
	titulo VARCHAR(200) NOT NULL,
	descripcion VARCHAR(500),
	estado VARCHAR(20) NOT NULL DEFAULT 'PENDIENTE' CHECK (estado IN ('PENDIENTE', 'EN_PROGRESO', 'COMPLETADO')),
	fecha_limite DATETIME,
	usuario_id BIGINT NOT NULL,
	fecha_creacion DATETIME DEFAULT GETDATE(),
	fecha_actualizacion DATETIME NOT NULL DEFAULT GETDATE(),
	FOREIGN KEY (usuario_id) REFERENCES usuario(ide_usr) ON DELETE CASCADE

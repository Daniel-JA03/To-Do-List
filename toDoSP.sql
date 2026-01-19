--------------------- USUARIO ---------------------

-- Verificar Inicio de Sesion

CREATE PROC sp_verificarLogin(
	@correo VARCHAR(150),
	@contraseña VARCHAR(255)
)
AS
BEGIN
	IF EXISTS (
		SELECT 1
		FROM usuario u
		WHERE u.cor_usr = @correo
			AND u.pwd_usr = @contraseña
	)
		SELECT 'approved' AS resultado;
	ELSE
		SELECT 'denied' AS resultado;
END
GO

sp_verificarLogin 'daniel@gmail.com', '123456'
GO

-- Registrar nuevo usuario
CREATE PROC sp_registrarUsuario (
	@correo VARCHAR(150),
    @contraseña VARCHAR(255),
    @nombre VARCHAR(100),
    @apellido VARCHAR(100)
)
AS
BEGIN
	INSERT INTO usuario (cor_usr, pwd_usr, nom_usr, ape_usr, fech_registro)
	VALUES (@correo, @contraseña, @nombre, @apellido, GETDATE())
END
GO

EXEC sp_registrarUsuario 'daniel@gmail.com','123456','Daniel','Jaimes';
GO

-- Obtener id del usuario
CREATE PROC sp_obtenerIdUsuario (
	@correo VARCHAR(150)
)
AS
BEGIN
	SELECT u.ide_usr
	FROM usuario u
	WHERE u.cor_usr = @correo
END
GO

sp_obtenerIdUsuario 'daniel@gmail.com'
GO

-- Existe correo
CREATE PROC sp_existeCorreo (
	@correo VARCHAR(150)
)
AS
BEGIN
	IF EXISTS (
		SELECT 1 FROM usuario WHERE cor_usr = @correo
	)
		SELECT 1
	ELSE
		SELECT 0
END
GO

sp_existeCorreo 'daniel@gmail.com' -- devuelve 1 si existe 
GO

--------------------- TAREA ---------------------
-- Listar tareas del usuario
CREATE PROC sp_listarTareasPorUsuario (
	@ide_usr BIGINT
)
AS
BEGIN
	SELECT 
		t.ide_tar,
		t.titulo,
		t.descripcion,
		t.estado,
        t.fecha_limite,
        t.usuario_id,
        t.fecha_creacion,
        t.fecha_actualizacion
	FROM tareas t
	WHERE t.usuario_id = @ide_usr
	ORDER BY fecha_creacion DESC

END
GO

sp_listarTareasPorUsuario 2
GO

-- Obtener tarea por ID
CREATE PROC sp_obtenerTareaPorId (
	@id BIGINT
)
AS
BEGIN
	SELECT
		t.ide_tar,
		t.titulo,
		t.descripcion,
		t.estado,
        t.fecha_limite,
        t.usuario_id,
        t.fecha_creacion,
        t.fecha_actualizacion
	FROM tareas t
	WHERE ide_tar = @id
END
GO

sp_obtenerTareaPorId 1
GO

-- Agregar tarea
CREATE PROC sp_agregarTarea (
	@titulo VARCHAR(200),
    @descripcion VARCHAR(500),
    @fecha_limite DATETIME,
    @usuario_id BIGINT
)
AS
BEGIN
	INSERT INTO tareas (titulo, descripcion, fecha_limite, usuario_id)
	VALUES (@titulo, @descripcion, @fecha_limite, @usuario_id)
END
GO

EXEC sp_agregarTarea 'probando', 'es una prueba', '2026-01-20 18:00:00', 2
GO


-- Actualizar tarea
CREATE PROC sp_actualizarTarea (
	@id BIGINT,
	@titulo VARCHAR(200),
    @descripcion VARCHAR(500),
    @estado VARCHAR(20)
)
AS
BEGIN
	UPDATE tareas 
	SET titulo = @titulo,
		descripcion = @descripcion,
		estado = @estado,
		fecha_actualizacion = GETDATE()
	WHERE ide_tar = @id;
END
GO

sp_actualizarTarea 1, 'Tarea actualizada', 'se actualizado la descripcion', 'EN_PROGRESO'

-- Eliminar tarea
CREATE PROC sp_eliminarTarea (
	@id BIGINT
)
AS
BEGIN
	DELETE
	FROM tareas
	WHERE ide_tar = @id
END
GO

-- sp_eliminarTarea 1

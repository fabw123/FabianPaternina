CREATE FUNCTION getPorcentajeTareasRealizadas 
(
	-- Add the parameters for the function here
	@IdMascota int,
	@FechaInicio Datetime,
	@FechaFin Datetime
)
RETURNS decimal(10,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @today Datetime = GETDATE();
	DECLARE @Total int;
	DECLARE @Realizadas int;
	DECLARE @Porcentaje decimal(10,2);

	-- Add the T-SQL statements to compute the return value here
	SELECT @Total = COUNT(*) FROM MascotaTarea mt WHERE mt.IdMascota = @IdMascota and mt.Fecha BETWEEN @FechaInicio AND @FechaFin;
	SELECT @Realizadas = COUNT(*) FROM MascotaTarea mt WHERE mt.IdMascota = @IdMascota and mt.Fecha BETWEEN @FechaInicio AND @today;
	IF @Total > 0 BEGIN
		SELECT @Porcentaje = (@Realizadas*100)/@Total;
	END
	ELSE BEGIN
		SELECT @Porcentaje = 0;		
	END
	
	-- Return the result of the function
	RETURN @Porcentaje;

END




CREATE PROCEDURE GetViewTareasHome
	-- Add the parameters for the stored procedure here
	@IdUsuario int,
	@FechaInicio Datetime,
	@FechaFin Datetime
AS
BEGIN
	select
		m.idMascota,
		m.nombre as Mascota,
		m.Apodo,
		r.nombre as Raza,
		sum(t.valor) as CostoTotal,
		max(mt.fecha) as FechaProximaTarea,
		(select Count(0) from MascotaTarea where idMascota = m.idMascota AND MascotaTarea.Fecha BETWEEN GETDATE() AND @FechaFin) Pendientes,
		 [dbo].[getPorcentajeTareasRealizadas](m.idMascota, @FechaInicio, @FechaFin) as PorcentajeRealizadas
	from Mascota m
	inner join Raza r on m.idRaza = r.idRaza
	inner join MascotaTarea mt on mt.idMascota = m.idMascota
	inner join Tarea t on t.idTarea = mt.idTarea
	inner join Usuario u on m.idusuario = u.idUsuario
	WHERE u.idUsuario = @idUsuario
	Group by m.idMascota, m.nombre,m.Apodo, r.nombre
END
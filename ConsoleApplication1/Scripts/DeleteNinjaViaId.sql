create proc dbo.DeleteNinjaViaId (@KeyVal int)
as
	delete dbo.Ninjas
	where Id = @KeyVal
GO
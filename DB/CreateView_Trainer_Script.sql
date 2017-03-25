USE [NidanProd]
GO

/****** Object:  View [dbo].[TrainerSearchField]    Script Date: 22/03/2017 04:31:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[TrainerSearchField]
AS 
SELECT 
		T.TrainerId,
		T.Name,
		T.Gender,
		T.AadharNo,
		T.Mobile,
		T.EmailId,
		T.CertificationNo,
		T.SectorId,
		T.CourseId,
		T.CentreId,
		T.OrganisationId,
		T.PersonnelId,
		T.CreatedDate,
		
		ISNULL(T.Name, '')+CONVERT(varchar, T.Mobile )+ISNULL(S.Name, '')+ISNULL(C.Name, '') AS SearchField
FROM 
	Trainer T  WITH (NOLOCK) left join Sector S WITH (NOLOCK)
	on T.SectorId = S.SectorId join Course C WITH (NOLOCK)
	on T.CourseId=C.CourseId

GO



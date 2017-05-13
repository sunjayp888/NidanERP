USE [NidanERP]
GO

/****** Object:  View [dbo].[CourseInstallmentSearchField]    Script Date: 13/05/2017 11:37:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[CourseInstallmentSearchField]
AS 
SELECT 

		C.CourseInstallmentId,
      C.Name,
      C.CourseId,
      C.Fee,
      C.DownPayment,
      C.LumpsumAmount,
      C.OrganisationId,
      C.NumberOfInstallment,
      C.CentreId,
      C.CreatedDate,

	  ISNULL(C.Name, '')+ISNULL(Co.Name, '')+CONVERT(varchar, C.Fee ) AS SearchField

	  FROM 
	CourseInstallment C WITH (NOLOCK) left join Course Co WITH (NOLOCK)
	on C.CourseId=Co.CourseId

	Go
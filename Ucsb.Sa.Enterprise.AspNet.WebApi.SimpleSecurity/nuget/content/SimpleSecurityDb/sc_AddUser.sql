--	pulling the UcsbCampusId can be cumbersome. Contact steven.maglio@sa.ucsb.edu if you
--	need to find a way.

insert into dbo.tbl_SimpleSecurityAccess (
	UcsbNetId, UcsbCampusId, Allowed
) values (
	'smaglio', 'cdedfbac-effe-11d3-8bb9-00a0c9202c0f', 1
)

--	in case you want to delete
-- delete from dbo.tbl_SimpleSecurityAccess where UcsbNetId = 'smaglio'
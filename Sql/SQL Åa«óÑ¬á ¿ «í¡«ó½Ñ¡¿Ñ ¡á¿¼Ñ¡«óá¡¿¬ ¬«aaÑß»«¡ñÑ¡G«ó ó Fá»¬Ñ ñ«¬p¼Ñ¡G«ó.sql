UPDATE [Document].Documents
SET AgentFromName = a.Name
FROM [Document].Documents d LEFT JOIN Contractor.Agents a
ON d.AgentFromId = a.Id
WHERE isnull(d.AgentFromName,'')<>a.Name

UPDATE [Document].Documents
SET AgentToName = a.Name
FROM [Document].Documents d LEFT JOIN Contractor.Agents a
ON d.AgentToId = a.Id
WHERE isnull(d.AgentToName,'')<>a.Name


UPDATE [Document].Documents
SET AgentDepartmentFromName = a.Name
FROM [Document].Documents d LEFT JOIN Contractor.Agents a
ON d.AgentDepartmentFromId = a.Id
WHERE isnull(d.AgentDepartmentFromName,'')<>a.Name

UPDATE [Document].Documents
SET AgentDepatmentToName = a.Name
FROM [Document].Documents d LEFT JOIN Contractor.Agents a
ON d.AgentDepartmentToId = a.Id
WHERE isnull(d.AgentDepatmentToName,'')<>a.Name


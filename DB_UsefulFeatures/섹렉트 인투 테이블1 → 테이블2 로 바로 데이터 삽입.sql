SELECT emp_id, name, salary
INTO #TempEmp
FROM employees
WHERE salary > 5000;

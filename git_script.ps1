# Запрос названия коммита у пользователя
$commitMessage = Read-Host -Prompt "Введите название коммита"

# Выполнение команд Git
git add .
git commit -m $commitMessage
git remote add origin https://github.com/dnjido/RTSGame.git
git push origin master
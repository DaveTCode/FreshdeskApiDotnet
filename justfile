# Commit lock files after restoring dotnet packages
commit-lock: restore-dotnet
	git commit *.lock.json -m "Update packages.lock.json"

# Restore lock files from git
restore-lock:
	git restore *.lock.json

# Restore staged lock files
restore-staged-lock:
	git restore *.lock.json --staged

# Continue rebase after restoring lock files
rebase-continue: restore-lock
	git rebase --continue

# Force restore dotnet packages
restore-dotnet:
	dotnet restore --force-evaluate

# Clear dotnet HTTP cache
reset-dotnet-cache:
	dotnet nuget locals -c http-cache

# Aliases
alias cl := commit-lock
alias rc := rebase-continue
alias rdc := reset-dotnet-cache

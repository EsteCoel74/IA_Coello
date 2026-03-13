@echo off
setlocal enabledelayedexpansion

echo ============================================
echo Configuration de l'application IA_Coello
echo ============================================
echo.
echo Cette clé API sera stockée comme variable d'environnement (sécurisée)
echo Pour plus d'infos: https://platform.openai.com/api-keys
echo.

set /p API_KEY="Entrez votre clé API OpenAI: "

if "%API_KEY%"=="" (
    echo Erreur: La clé API ne peut pas être vide.
    pause
    exit /b 1
)

REM Configurer la variable d'environnement pour l'utilisateur actuel (persistante)
setx OPENAI_API_KEY "%API_KEY%"

echo.
echo ✓ Variable d'environnement OPENAI_API_KEY configurée avec succès!
echo.
echo IMPORTANT: Redémarrez Visual Studio pour que le changement soit appliqué.
echo.
pause

# Bamboo Shield
Is a [NancyFX](https://github.com/NancyFx/Nancy) / .Net based web that provides shields.io like badges for Atlassian Bamboo build server builds using it's Rest API.

This project currently is at a early proof of concept state, still needs testing and likely has bugs.

## Configuration
The web is configured against your on-prem / cloud Bamboo instance via environment variables.

|Variable name            |Example                                                         |
|------------------------:|----------------------------------------------------------------|
|BAMBOOSHIELD_API_BASE_URL| _on-prem:_ http://myhost.com:8085/bamboo/rest/api/latest/      |
|                         | _cloud:_ https://mydomain.atlassian.net/builds/rest/api/latest |
|BAMBOOSHIELD_API_LOGIN   |john                                                            |
|BAMBOOSHIELD_API_PASSWORD|password                                                        |

## Usage
Once web is up and configured you can get an SVG badge for a plan using following route:

`bambooshield.myhost.com/planstatus/{style}/{projectKey}-{buildKey}.svg`

|Parameter  |Example                   |
|----------:|--------------------------|
|style      | Flat, FlatSquare, Plastic|
|projectKey | BAMSHI                   |
|buildKey   | BUILD                    |

Example url:
`https://bambooshield.azurewebsites.net/planstatus/Flat/BAMSHI-BUILD.svg`

![Bamboo shield](https://bambooshield.azurewebsites.net/planstatus/Flat/BAMSHI-BUILD.svg)

# WotBlitzStatisticsPro
Detailed player &amp; tank statistics and history for [World of Tanks: Blitz](https://wotblitz.com/)

- Uses [Wargaming API Service](https://developers.wargaming.net/documentation/guide/getting-started/) for gathering players statistics
- Stores statistics snapshots in local database
- Allows to view players, their tanks and achievements statistics, and how it was change in time

## Run as docker container

```bash
docker build -t wotblitzstatisticspro .
docker run -dp 8840:80 wotblitzstatisticspro --env WargamingApi:ApplicationId=[ypur app id] --env WargamingApi:BlitzApiUrl=[real-url] --env WargamingApi:WotApiUrl=[real-url]
```
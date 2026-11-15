#!/usr/bin/env bash
# EgoNet Revival - GRID Autosport RaceNet server (Linux).
# Run from anywhere: data/, certs/ and logs/ are created next to this script.
#
# Ports 80 and 443 need root, or once:
#   sudo setcap 'cap_net_bind_service=+ep' ./RaceNetShowdown.Server
# Other ports can be set with: RaceNet__HttpPort=8080 RaceNet__HttpsPort=8443 ./start-server.sh
set -e

cd "$(dirname "$(readlink -f "$0")")"

# Archives built on Windows do not keep the executable bit.
[ -x ./RaceNetShowdown.Server ] || chmod +x ./RaceNetShowdown.Server

export ASPNETCORE_ENVIRONMENT=GridAutosportHost
exec ./RaceNetShowdown.Server "$@"

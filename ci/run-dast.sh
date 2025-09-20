#!/bin/bash
docker run --rm -v $(pwd)/docs:/zap/wrk/:rw \
  owasp/zap2docker-stable zap-baseline.py \
  -t http://localhost:5000 \
  -r zap-report.html
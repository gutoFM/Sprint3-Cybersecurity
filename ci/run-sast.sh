#!/bin/bash
semgrep --config auto \
        --config ./ci/semgrep-rules/custom-rule.yml \
        Sprint3-CSHARP/ \
        --json > ./docs/sast-report.json
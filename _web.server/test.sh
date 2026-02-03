# Sample curl requests

rm -rf tmp
mkdir tmp

curl "http://localhost:4250/mock" > tmp/mock.json
curl -H "content-type: text/csv" "http://localhost:4250/mock" > tmp/mock.csv
curl -H "content-type: text/csv" "http://localhost:4250/lineChart" > tmp/lineChart.csv
curl -H "content-type: text/csv" "http://localhost:4250/pieChart" > tmp/pieChart.csv
curl -X POST "http://localhost:4250/login" > tmp/login.post.json


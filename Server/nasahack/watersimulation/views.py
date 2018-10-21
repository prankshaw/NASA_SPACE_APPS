from django.http import HttpResponse, JsonResponse
import time as Time
from watersimulation.models import *

def val_put(request):
    place = request.GET.get("point", "1");
    if place == "1": place = "1"
    elif place=="2": place = "2"
    elif place == "3": place = "3"
    elif place == "4": place ="4"
    elif place == "5": place = "5"
    cwl = request.GET.get("cl", "#")
    dwl = request.GET.get("dl", "#")
    if cwl=="#" or dwl == "#":
        return HttpResponse("Error")
    ds = Dataset()
    ds.place = place
    ds.cwl = cwl
    ds.dwl = dwl

    ds.save()
    return HttpResponse("ok")

import json
def val_get(request):
    ds = Dataset.objects.all()
    data=[]
    for s in ds:
        data.append({'place':s.place, 'time': str(s.time), 'cwl': s.cwl, 'dwl':s.dwl})
    return HttpResponse(json.dumps(data))

from watersimulation.background import start
def start_pred(request):
    start()
    return HttpResponse("started")
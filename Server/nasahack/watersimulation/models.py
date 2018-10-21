from django.db import models

# Create your models here.
class Dataset(models.Model):
    time = models.DateTimeField(auto_now=True)
    place = models.TextField()
    cwl = models.FloatField()
    dwl = models.FloatField()
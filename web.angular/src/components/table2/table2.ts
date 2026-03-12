import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { TableColumn } from '../table/tableColumn';
@Component({
  selector: 'nc-table2',
  imports: [CommonModule],
  templateUrl: './table2.html',
  styleUrl: './table2.css',
  standalone: true,
})
export class Table2 {
  @Input() columns: TableColumn[] = [];
  @Input() dataList: any[] = [];

}

import { CommonModule } from '@angular/common';
import { Component, Input, TemplateRef } from '@angular/core';
import { TableColumn } from './tableColumn';

@Component({
  selector: 'nc-table',
  imports: [CommonModule],
  templateUrl: './table.html',
  styleUrl: './table.css',
})
export class Table {
  @Input() columns: TableColumn[] = [];
  @Input() dataList: any[] = [];
  cellTemplates = new Map<string, TemplateRef<any>>();

  getCellContext(row: any, col: TableColumn): { $implicit: any; row: any } {
    return { $implicit: row[col.key], row };
  }

  ngAfterContentInit(): void {
    this.syncTemplates();
  }

  private syncTemplates(): void {
    this.cellTemplates.clear();
    this.columns.forEach((col) => {
      this.cellTemplates.set(col.key, col.template);
    });
  }
}
